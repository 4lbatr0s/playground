
/**
 * @tasks
 */

//Hello object.
const user = {};
user.name = "John";
user.surname = "Smith";
user.name = "Pete";
delete user.name;
console.log(user);


//Write the function isEmpty(obj) which returns true if the object has no properties, false otherwise.

const isEmpty = (obj) => {
    for(let key in obj)
    {
        return false;//INFO: if the loop has started, there is a property!
    }
    return true;
}

let schedule = {};

console.log( isEmpty(schedule) ); // true

schedule["8:30"] = "get up";

console.log( isEmpty(schedule) ); // false


//Sum object properties.

let salaries = {
    John: 100,
    Ann: 160,
    Pete: 130
}

const sum = (obj) => {
    let count = 0; //INFO: if the object is null, it does not go into loop.
    for (let key in obj) {
        count+=obj[key]     
    }
    return count;
}

console.log(sum(salaries))


//Create a function multiplyNumeric(obj) that multiplies all numeric property values of obj by 2.
// before the call
let menu = {
    width: 200,
    height: 300,
    title: "My menu"
  };


const multiplyNumeric = (object) => {
    for (let key in object) {
        if(!isNaN(object[key])) //INFO: if isNaN returns false, the value is a number.
        {
            let temp = object[key] * 2;
            object[key] = temp;
        }
    }
}

multiplyNumeric(menu);
console.log(menu);