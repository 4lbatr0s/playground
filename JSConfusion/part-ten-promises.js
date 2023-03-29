
// //INFO: PROMISE: AN OBJECT THAT INVOLVES THE RESULT OF AN ASYNCHRONOUS JAVASCRIPT ACTION!

// const promise = new Promise((resolve, reject) => {
//     //TIP: this function called "e"
//     // resolve();
//     reject("ERROR");
// });
// console.log(promise);

// //2.Promise involves 3 different situations, pending, fullfilled, rejected

// //2.1.Rejected or Resolved promise cannot be undone, cannot go from rejected=>resolved, resolved=rejected, rejected&resolved =>pending.

// const promise1 = new Promise((resolve, reject) => {
//     // resolve("NOT UNDOABLE");
//     reject("ERROR");
// });
// console.log(promise1);//returns "NOT UNDOABLE"..


// //3.INFO: HOW TO CONSUME PROMISES:
// // promise1.then((value)=>{ //TIP: VALUE => Data/callback that resolve returns, in this case "NOT UNDOABLE"
//     // console.log(value);
// // })
// promise1.then(value=>console.log(value));//same

// //INFO:3.1. HOW TO CATCH ERRORS WITH PROMISE

// promise1.catch(reason=>console.log(reason));



// //INFO:3.2. HOW TO USE CATCH - THEN TOGETHER:

// promise1.then(()=>{
//     console.log("VERILER ALINDI"); 
// }, ()=>{
//     console.log("VERILER ALINMADI");
// });


// promise1.then(()=>{
//     console.log("veriler alindi");
// }).catch(()=> {
//     console.log("veriler alinmadi");
// });


// INFO: 4.PROMISE BOOK EXAMPLE:

const books = [
    {name: "Pinball 1973", author: "Haruki Murakami"},
    {name: "Özgürlük", author: "Zygmunt Bauman"},
    {name: "Turkiye'de Çağdaşlaşma", author:"Niyazi Berkes"}
]

const listBooks = () => {
    books.map((book, index) => {
        console.log(book, index)
    })
} 

const addBookPromise = (newBook)=>{
    const promise1=new Promise((resolve, reject)=> {
        books.push(newBook);
        resolve(books);
        reject("hata");
    });
    return promise1;
}

const alicaInWonderLand = {name:"Alice in Wonderland", author:"Lewis Caroll"};

addBookPromise(alicaInWonderLand).then((result)=> {
    console.log("New List:");
    listBooks(result);
}).catch((err)=> {console.log(err)});



// 5.INFO: Another example:
// const addTwoNumbers = (num1, num2) => {
//     const promise2 = new Promise((resolve, reject) => {
//         const sum = num1 + num2;
//         (typeof num1 !== 'number' || typeof num2 !== 'number')
//         ? reject('2 SAYI GİRMENİZ GEREKİR')
//         : resolve(sum)
//     })
//     return promise2
// }

// addTwoNumbers(4,54)
// .then((value) => {
//     console.log('TOPLAM: ', value)
// })
// .catch((reason) => {
//     console.log('HATA: ', reason)
// })
 