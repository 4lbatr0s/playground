class UrlHelper{
    constructor(){}

    getFishWatchUrl(species){
        return `https://www.fishwatch.gov/api/species/${species}`;
    }
}

export default new UrlHelper();