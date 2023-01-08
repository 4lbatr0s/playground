

/**
 * @reachability is the key for memory management in the JS.
 */

let user = {
    name:"Serhat"
}

//TIP: when we change reference and name becomes unreachable, garbage collector removes it from the memory.

user = null;