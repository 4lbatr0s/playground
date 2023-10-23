import express from "express"; 
import FishRoutes from "./api-routes/Fish.js";
import redis from "redis";

const app = express();

app.use('/fishes', FishRoutes);

const port = 3000;

//create redis client

const redisClient = redis.createClient(); //create the client object.
redisClient.on('error', (err)=> console.log('Error:',err)); //Node.js on method registers events on the redisClient
await redisClient.connect();//connects redis to the port, in this case 6379 default.
export {redisClient};//to use redis client in Fishes

app.listen(port, ()=> {
    console.log(`App listening on ${port}`);
})

