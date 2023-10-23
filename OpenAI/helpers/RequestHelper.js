import {Configuration, OpenAIApi} from "openai";
import Prompts from "./Prompts.js";

class RequestHelper {
    async sendPrompt(article) {
        const configuration = new Configuration({
            apiKey: process.env.API_KEY,
        });
        const openai = new OpenAIApi(configuration);

        const response = await openai.createCompletion({
            model: 'text-davinci-003',
            prompt: `${Prompts["5WsAnd1H"]}${article}`,
            temperature: 0.7,
            max_tokens: 60,
            top_p: 1.0,
            frequency_penalty: 0.0,
            presence_penalty: 1,
        });
        const resArr = response.data.choices[0].text.trim('').split("/");
        return {
            Who:resArr[0].replace('Who',''),
            What:resArr[1].replace('What',''),
            When:resArr[2].replace('When',''),
            Where:resArr[3].replace('Where',''),
            Why:resArr[4].replace('Why',''),
            How:resArr[5].replace('How',''),
        }
    }
}
export default new RequestHelper();