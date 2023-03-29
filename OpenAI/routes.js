import express from 'express';
import { OpenAIApi, Configuration } from 'openai';
import Prompts from './helpers/Prompts.js';
import RequestHelper from './helpers/RequestHelper.js';

const router = express.Router();

router.post('/summarize', async (req, res) => {
    try {
        const response = await RequestHelper.sendPrompt(req.body?.article);
        res.status(200).send(response); 
    } catch (error) {
        return res.status(500).send({ message: error.message });
    }
});

export default router;
