import express, { json } from "express";
import jwt from "jsonwebtoken";

const app = express();


app.use(express.json())

const users = [
    {
        id:1,
        username:'john',
        password:'john0908',
        isAdmin:true
    },
    {
        id:2,
        username:'jane',
        password:'jane0908',
        isAdmin:false
    },

];

const generateAccessToken = (user) => {
    const accesToken = jwt.sign({id:user.id, isAdmin:user.isAdmin}, "mySecretKey", {expiresIn:'15m'}); //expires in 20 secs.
    return accesToken;
};

const generateRefreshToken = (user) => {
    const refreshToken = jwt.sign({id:user.id, isAdmin:user.isAdmin}, "myRefreshSecretKey", {expiresIn:'15m'}); //expires in 20 secs.
    return refreshToken;
};

//normally we should store our refresh token in the cache or database.
let refreshTokens = [];


app.post("/api/login", (req, res) => {
    const { username, password } = req.body;
    const user = users.find((u) => {return u.username === username && u.password === password});
    if(user){
        const accesToken = generateAccessToken(user);
        const refreshToken = generateRefreshToken(user);
        refreshTokens.push(refreshToken);
        res.json({
            username:user.username,
            isAdmin:user.isAdmin,
            accesToken,
            refreshToken
        });
    } else {
        res.status(400).json("Username or password incorrect");
    }
});


const verify = (req,res,next) => { 
    const authHeader = req.headers.authorization;
    if(authHeader){
        const token = authHeader.split(" ")[1];
        jwt.verify(token, "mySecretKey", (err, user) => {
            if(err){
                return res.status(403).json('Token is not valid');
            }
            req.user = user; //Eger token validse, verificationdan donen useri requestin userine setleriz.
            next();
        })
    } else {
        res.status(401).json("You are not authenticated.");
    }
}


app.delete("/api/users/:userId", verify, (req, res) => {
    if(req.user.id === req.params.userId || req.user.isAdmin){
        res.status(200).json("User has been deleted");
    } else { 
        res.status(403).json("You are not allowed to delete this user");
    }
});



//INFO: HOW TO REFRESH TOKEN
app.post("/api/refresh", (req, res) => { 
    //take refresh token from user.
    const refreshToken = req.body.token;
    //send error if token is not valid.
    if(!refreshToken){
        return res.status(401).json("You are not authenticated");
    } 
    if(!refreshTokens.includes(refreshToken)){
        return res.status(403).json("Refresh token is not valid");
    }
    jwt.verify(refreshToken, "myRefreshSecretKey", (err, user)=> {
        err && console.log(err);
        //INVALIDATE OLD ACCESS TOKEN AND CREATE A NEW ONE.
        refreshTokens = refreshTokens.filter(token => token !== refreshToken);
        const newAccessToken = generateAccessToken(user);
        const newRefreshToken = generateRefreshToken(user);
        refreshTokens.push(newRefreshToken);
        res.status(200).json({
            accesToken:newAccessToken,
            refreshToken:newRefreshToken
        })
    })
    //if everything is okay, create new access token and refresh token, then send to user.
});


app.post("/api/logout", verify, (req, res) => {
    const refreshToken = req.body.token;
    refreshTokens = refreshTokens.filter(token !== refreshToken);
    res.status(200).json("You logged out successfully");
})

app.listen(5000, () => console.log("Backend server is running"));

