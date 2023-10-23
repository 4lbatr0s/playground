import express from 'express';
import FishController from '../controllers/Fish.js';
import redisCache from "../middlewares/caching/redis.js";

const router = express.Router();
router.get(`/:species`, redisCache, FishController.getFishes);


export default router;