import express from "express";
import dotenv from "dotenv"
import PromptRoutes from "./routes.js";
import {OpenAIApi} from "openai";

const app = express();
dotenv.config();
export const client = new OpenAIApi(process.env.API_KEY);
app.use(express.json())
app.use("/",PromptRoutes);

app.listen(3000, ()=>{
    console.log("listens on:", 3000);
})