let user = {name:"John"};
let admin = user; //INFO: now they show the same reference.

admin.name = "Pyetr";
console.log(user.name);