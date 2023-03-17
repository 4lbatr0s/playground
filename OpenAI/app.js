import express from "express";
import dotenv from "dotenv"
import PromptRoutes from "./routes.js";

const app = express();

dotenv.config();

app.use("/",PromptRoutes);

app.listen(3000, ()=>{
    console.log("listens on:", 3000);
})