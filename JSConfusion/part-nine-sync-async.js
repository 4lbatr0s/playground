//INFO:Asenkron JS, Call Stack, Thread, Callback Fonsiyonlar

//1.TIP: JAVASCRIPT WORKS AS A SINGLE THREAD AND SYNCHRONOUS
// const func1 = () => {
// console.log("Func 1 Console Log 1");
// console.log("Func 1 Console Log 2");
// alert('Alert Message');
// }
// const func2 = () => {
// console.log("Func 2 Console Log 1");
// console.log("Func 2 Console Log 2");
// }

// func1();
// func2();

//2.TIP: IMAGINE WE HAVE TO WAIT FOR AN OPERATION SUCH AS SETTIMEOUT.
// let x = 10;
// console.log("1. Gelen Veri", x);
// setTimeout(() => { //INFO: WAITS IN THE WEB API FOR A SECOND THEN BEING SENT TO QUEUE
// x = x + 5;
// console.log("3. Gelen Veri", x);
// }, 1000)
// console.log("2. Gelen Veri", x);
//3.TIP: CALL STACK: FUNCTION EXECUTION ORDER
// function func1(){
//     console.log("im the first!")
//     func2();
//     console.log("im the first again !")
// }

// function func2(){
//     console.log("im the second!")
//     func3();
//     console.log("im the second again !")
// }

// function func3(){
//     console.log("im the third!")
// }

// func1();

//4.INFO: CALLBACK FUNCTIONS
// const myName = () => {
// console.log("my name is serhat");
// }
// setTimeout(myName, 3000);//EXECUTE myName after 3 seconds

//4.1. INFO:CALLBACKS ARE MOSTLY USED IN EVENT LISTENERS!
// const btn = document.querySelector('button');
// btn.addEventListener('click', ()=> {
// alert("you clicked me!");

// let pElem = document.createElement('p');
// pElem.textContent = "This is a newly added paragraph";
// document.body.appendChild(pElem);
// })

//4.2.INFO:

// const books = [
//     {name: "Pinball 1973", author: "Haruki Murakami"},
//     {name: "Özgürlük", author: "Zygmunt Bauman"},
//     {name: "Turkiye'de Çağdaşlaşma", author:"Niyazi Berkes"}
// ];

// const listBooks = () => {
//     books.map((book, index) => {
//         console.log(book, index)
//     })
// };

// const addNewBook = (newBook, callback) => {
//     books.push(newBook);
//     callback();
// };

// addNewBook({name:"Berlin Hatiralari", author:"Husrev Gerede"}, listBooks);

//5.INFO: PROMISES:
const getUsers = (callback) => {
    let users = [];
    setTimeout(() => {
        users = [
            { name: 'serhat', age: 24 },
            { name: 'vel', age: 30 },
        ];
        callback(users); //Send users to the callback as an argument.
    }, 1000);
};

const findUser = (name, callback) => {
    //()=> .... is our callback function
    getUsers((users) => {
        //users is the parameter that we pass in the getUsers
        const user = users.find((user) => user.name === name);
        callback(user);
    });
};

findUser('serhat', console.log);

function getUsersWithPromise() {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve([
                //callback is the first function after then created.
                { username: 'john', email: 'johndope@gmail.com' },
                { username: 'jane', email: 'janedope@gmail.com' },
            ]);
        }, 1000);
    });
}
function onFullFilled(users) {
    console.log(users);
}
const promise = getUsersWithPromise();
promise.then(onFullFilled);

/*
5.1.INFO:n this example, the getUsers() function always succeeds.
To simulate the error, we can use a success flag like the following:
*/

let success = false;
function getUsersWithPromiseWithReject() {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            if (success) {
                resolve([
                    //callback is the first function after then created.
                    { username: 'john', email: 'johndope@gmail.com' },
                    { username: 'jane', email: 'janedope@gmail.com' },
                ]);
            } else {
                reject("Failed to the user list!");
            }
        }, 1000);
    });
}

function onRejected(error) {
    console.log(error);
}
const promise2 = getUsersWithPromiseWithReject();
promise.then(onFullFilled, onRejected);



//5.3.INFO: A real life example of promises in JS:
function getJsonValue(url){
    return new Promise((resolve, reject)=> {
        var xhr = new XMLHttpRequest(); 
        xhr.onreadystatechange = function(){
            //this here is not part of JS, its part of XMLHttpRequestAPI, therefore this here belongs to XMLHttpRequest
            if(this.readyState === XMLHttpRequest.DONE && this.status === 200){
                resolve(this.response);
            } else {
                reject(this.status);
            }   
        }
        xhr.open("GET",url,true);
        xhr.send();
    })
}

const requestBtn = document.querySelector("#btnGet");
requestBtn.addEventListener("click",()=> {
    let jsonContainer  = document.querySelector("#jsonContainer");
    let promise = getJsonValue("https://www.javascripttutorial.net/sample/promise/api.json");
    promise.then((response)=>{
        const result = JSON.parse(response);
        jsonContainer.innerHTML = result.message;
    })
    .catch((error)=> {
        jsonContainer.innerHTML = `Error, HTTP Status:${error}`;
    })
})
