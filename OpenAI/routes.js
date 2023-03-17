import express from 'express';
import { OpenAIApi, Configuration } from 'openai';

const router = express.Router();

router.post('/prompt', async (req, res) => {
    try {
        const configuration = new Configuration({
            apiKey: process.env.API_KEY,
        });
        const openAi = new OpenAIApi(configuration);
        const response = await openAi.createCompletion({
            model: 'text-davinci-003',
            prompt: req.body?.prompt,
            temperature: 0,
            max_tokens: 100,
            top_p: 1,
            frequency_penalty: 0.0,
            presence_penalty: 0.0,
            stop: ['\n'],
        });
        return res.status(200).send(response.data.choises[0].text);
    } catch (error) {
        return res.status(500).send({message:error.message});
    }
});


export default router;
