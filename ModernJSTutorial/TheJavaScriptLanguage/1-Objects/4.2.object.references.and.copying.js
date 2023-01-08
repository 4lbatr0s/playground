let user = {name:"John"};
let admin = user; //INFO: now they show the same reference.

admin.name = "Pyetr";
console.log(user.name);


/**
 * @cloning 
 * @merging 
 * @ObjectAssign
 */

//INFO: We can duplicate an object by iterating its key values.

let user2 = {
    name:"John",
    age:30
}

let clone = {};

for (const key in user2) {
    clone[key] = user2[key];
}

clone.name = "Pete";
console.log(user2.name);
console.log(clone.name);

//TIP: We can also use the method Object.assing(destination, sources (can be more than one))
let user3 = {name:"John"}
let permission1 = {canView:true};
let permission2 = {canEdit:true};
Object.assign(user3, permission1, permission2);
console.log(user3);
//TIP: how to clone a single object with Object.assing:
let user4 = {name:"Serhat"};
const clone2 = Object.assign({}, user4);
console.log(clone2);


//INFO: structureClone()
let user5 = {
    name: "John",
    sizes: {
      height: 182,
      width: 50
    }
  };
  
  let clone3 = Object.assign({}, user5);
  
  console.log( user5.sizes === clone3.sizes ); // true, same object
  // user5 and clone3 share sizes
  user5.sizes.width = 60;    // change a property from one place
  console.log(clone3.sizes.width); // 60, get the result from the other one

//TIP: Object.asssing is not adequate, therefore structuredClone() should be used.
let user6 = {
    name: "John",
    sizes: {
      height: 182,
      width: 50
    }
};

let clone4 = structuredClone(user6) 
console.log(user6.sizes === clone4.sizes); //false, not the same object.

clone4.sizes.height = 500;
console.log(user6.sizes.height);

//TIP: structureClone does not work when the object involves a function.

