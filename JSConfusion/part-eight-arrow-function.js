
/* ARROW FUNCTION */

//1.INFO:Function Declaration

/* console.log(square(4));
function square(num) {
    return (num*num);
}
// Function Expression
const square2 = function(num) {
    return (num*num)
}
console.log(square2(6));
// Arrow Function Expression
//const square3 = function(num) { return (num*num) };
//const square3 = (num) => { return (num*num) };
const square3 = num => (num*num);
console.log(square3(8));
*/


//2.INFO: Relationship between THIS and ARROW function.

//2.1. global this
window.name = 'serhat';
function tellName(){
    console.log(this); //window
    console.log(this.name);//window.name
}
tellName();

//2.1.2.object this use cases!
const person = {
    name:'anima',
    tellName:function(){
        console.log(this); //person object
        console.log(this.name);//anima
    },
    //INFO: Arrow functions does not have their own this keywords. It always indicates one block up.
    tellNameArrow:()=>{
        console.log("arrow this:",this); //global object,
        console.log(this.name);//global object!
    },
    tellNameSettimeOut:function(){
        console.log("this:",this); //person object
        console.log(this.name); //anima

        setTimeout(function(){ //TIP: For setTimeout is not a function of Person but, global object. it will output global object.
            console.log("thisSettimeout:",this);
            console.log(this.name);        
        }, 2000)
    },
    tellNameSettimeOutWithArrowFunc:function(){
        console.log("this:",this); //person object
        console.log(this.name); //anima

        setTimeout(()=>{ //TIP: For setTimeout is not a function of Person but, global object. it will output global object.
            console.log("thisSettimeout:",this);//brings one up, so get out of person block, you get window GLOBAL OBJECT!
            console.log(this.name);//RETURNS serhat
        }, 2000)
    }
}

person.tellNameArrow();//global


//INFO: 3.ARROW FUNCTIONS CANNOT BE USED WITH ARROWS BECAUSE IT DOESN'T HAVE A THIS KEYWORD



//INFO: 4.DO NOT USE ARROW FUNCTIONS AS A METHOD OF AN OBJECT, OR A CLASS, BECAUSE IT POINTS ONE BLOCK UP FOR THIS KEYWORD!


//INFO: 5.DO NOT USE ARROW FUNCTIONS WITH CALL, BIND AND APPLY

//5.1. TIP: CALL AND THIS RELATION:
const student = {
    examResult(){
        return this.name  + ' ' + this.grade
    },
    examResultWithArrow:()=>{
        return this.name  + ' ' + this.grade
    }
}
const student1 = {
    name:"serhat",
    grade:70
}
const student2 = {
    name:"oner",
    grade:80
}
console.log(student.examResult().call(student1));
console.log(student.examResult().call(student2));
console.log(student.examResultWithArrow().call(student1));//TIP: WILL CALL THE GLOBAL THIS!
console.log(student.examResultWithArrow().call(student2));//TIP: WILL CALL THE GLOBAL THIS!

