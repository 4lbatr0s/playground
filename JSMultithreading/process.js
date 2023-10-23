/**
 * process.argv: returns an array containing the program command line arguments.
 * slice(2), don't take first two arguments, Node.js path and program filename.
 */
const process_name = process.argv.slice(2)[0];

count = 0;
while(true){
    count++;
    if(count==2000 || count == 4000){
        console.log(`${process_name}: ${count}`);
    }
}