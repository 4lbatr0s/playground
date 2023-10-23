import redis from "redis";

class BaseController {
    constructor() {
        this.redisClient = redis.createClient(); // create the client object
        this.redisClient.on('error', (err) => console.log('Redis error:', err)); // register error event
        this.redisClient.on('connect', () => console.log('Redis connected')); // register connect event
    }
}

export default BaseController;
