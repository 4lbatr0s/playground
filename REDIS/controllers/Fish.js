import AxiosHelper from '../helpers/AxiosHelper.js';
import { redisClient } from '../app.js';

class FishsController {
    constructor() {}

    async getFishes(req, res) {
        const species = req.params?.species;
        let results;
        try {
            results = await AxiosHelper.fetchApiData(species);
            if (results.length === 0) {
                throw 'API RETURNED AN EMPTY ARRAY';
            }
            await redisClient.set(species, JSON.stringify(results), {
                EX: 180, //expiration 180 secs.
                NX: true, //set a key value that does not exist in Redis
            }); //TIP: because by default redis stores values as strings
            return res.status(200).send({
                fromCache: false,
                data: results,
            });
        } catch (error) {
            throw new Error(error?.message, error?.statusCode);
        }
    }
}

export default new FishsController();
