


//INFO: this keyword

/**
 * THE THING THAT THIS OBJECT POINT OUTS DEPENDS ON THE USAGE OF THIS:
 */

//1. When calling a method of object, it refers the object.


//2.In most of the cases this refers the global object ===> in node.js, global and in browser window
console.log(this);
console.log(this.name);
console.log(this.age);


//TIP: 2.1.FUNCTION DECLARATION
function func1(){
    this.name = "Serhat"; //TIP: GOES TO GLOBAL OBJECT
    console.log(this);//TIP: INDICATES THE GLOBAL OBJECT!
    console.log(this.location);
    console.log(this.location.href);
    console.log(this.age);//UNDEFINED, because FUNC2 is undefined because FUNCTION EXPRESSION DOES HAVE HOISTING.
}

//TIP: 2.2.FUNCTION EXPRESSION
const func2 = function(){
    console.log(this.name); //RETURNS SERHAT!
    this.age=40; //TIP: GOES TO GLOBAL OBJECT
    console.log(this);//TIP: INDICATES THE GLOBAL OBJECT!
    console.log(this.location);
    console.log(this.location.href);
}
//here we get serhat and undefined.
console.log("-----------------------------------");
func1();
func2(); 

//here, we will reach "serhat" and age 40.
console.log("-----------------------------------");
func2();
func1();




//INFO:3.THIS KEYWORD IN CONSTRUCTOR FUNCTION:
//TIP: THIS INDICATES THE CREATED OBJECT!
/* function Person(name, surname, age) {
    const obj = {};
    console.log(obj);
    obj.name = name;
    console.log(obj);
    obj.surname = surname;
    console.log(obj);
    obj.age = age;
    obj.fullName = function() {
        return obj.name + " " + obj.surname
    }
    return obj;
} */


/* function Person(name, surname, age) {
    this.name = name;
    this.surname = surname;
    this.age = age;
    this.fullName = function() {
        return this.name + " " + this.surname
    }
} */

/* class Person {
    constructor (name, surname, age) {
        this.name = name;
        this.surname = surname;
        this.age = age;
    }
    fullName = function() {
        return this.name + " " + this.surname
    }
*/

//INFO: 4.THIS IN OBJECT FUNCTIONS!

/* const arin = {
    name: "Arin",
    age: 5,
    surname:"Çekiç",
    fullName:  function() {
        console.log(this);
        //return this.name + " " + this.surname
    },
    mother : {
        name: "Gamze",
        age:40,
        surname:"Çekiç",
        fullName:  function() {
            console.log(this);
            //return this.name + " " + this.surname
        }
    }
}
console.log(arin.fullName());  //OUTPUTS arin object
console.log(arin.mother.fullName()); */ //OUTPUTS mother object!

/* const elis = {
    name: "Elis",
    funcy: function() {
        console.log(this === elis);
        console.log(this === window);
    }
}
//console.log(elis.funcy()); //TIP: RETURNS TRUE,FALSE
const funcy2 = elis.funcy;  //TIP: WE'VE ASSIGNED FUNCY TO A FUNCY2 VARIABLE IN THE GLOBAL.
console.log(funcy2()); */ //TIP: RETURNS FALSE,TRUE BECAUSE FUNCY2 IS NOT A FUNCTION OF THE OBJECT!

const elis = {
    name: "Elis",
    funcy: function() {
        console.log(this === elis); //TIP:TRUE
        setTimeout(function() { 
            console.log(this === elis);  //TIP:FALSE
            console.log(this === window); //TIP:TRUE, BUT WHY ? INFO: SETTIMEOUT IS NOT ELIS, ITS A PART OF GLOBAL OBJECT!
        }, 2000);
    }
}
console.log("'''''''''''''''")
console.log(elis.funcy());