import { redisClient } from "../../app.js";

async function cacheData(req,res,next){
    const species = req.params?.species;
    if (!species) throw new Error('Species not specified', 500);
    let results;
    try {
        const cacheResults = await redisClient.get(species);
        if(cacheResults){
            results = JSON.parse(cacheResults); //TIP: redis holds values with string format by default.
            return res.status(200).send({
                fromCache:true,
                data:results
            })
        } else {
            return next();
        }
    } catch (error) {
        throw new Error(error?.message, 404);
    }
}

export default cacheData;