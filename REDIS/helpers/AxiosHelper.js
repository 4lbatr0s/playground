import axios from "axios";
import UrlHelper from "./UrlHelper.js";

class AxiosHelper{
    constructor(){}
    async fetchApiData(species){
        const apiResponse = await axios.get(UrlHelper.getFishWatchUrl(species));
        console.log('Request sent to the API');
        return apiResponse.data;
    }
}

export default new AxiosHelper();