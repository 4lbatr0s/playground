## What is a process?

1. Its a program running on the cpu.
2. Its has its own memory and cannot see or access other running programs(processess)
3. Its got an "instructor pointer", this pointer indicates the task currently executing in the program(process).
4. Only one task can be executed at a time.

## Running process
1. When you run *node* command you create a *process*.

### How a process created?

1. Operating system allocates a memory for the process.
2. Operating system locates program executable on your computer's disk.
3. Loads the program into memory.
4. Assigns program to a process ID.
5. Then executes the program, at this point your program becomes a process. 

## What are Threads? 

1. Threads are like processes, but they don't have their own resource, they use processes' resources.
2. When you create a process, it can have multiple threads.
3. Threads can communicate with each other through sharing data or messaging in the process's memory.

# IS NODE JS SINGLE THREADED? 
1. JavaScript is a single threaded language.
2. Node.js has 4 hidden threads for every node process.
3. These 4 hidden threads are provided by the *LIBUV* library.
4. Also V8 engine provides 2 extra threads for handling things like garbage collector etc.
5. *So total thread number in a node js application is 7. 1 main thread, 4 hidden node js threads and two V8 engine threads*

