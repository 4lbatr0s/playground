import amqp from "amqplib";
import data from "./data.json" assert { type: "json" }; //INFO: HOW TO IMPORT JSON FILES IN NODE JS.

const queueName = process.argv[2] || 'jobsQueue';


const message = {
    description:"its a test message"
}


connect_rabbitmq();//hoisting.

//TIP: 1.First we should connect to AMQP for RabbitMQ uses AMQP.
async function connect_rabbitmq(){

    try {
        const connection = await amqp.connect("amqp://localhost:5672");//its default port.
        const channel = await connection.createChannel(); //create the channel
        const assertion = await channel.assertQueue(queueName);//assert the queue,

        data.forEach(i=>{
            message.description = i.id;
            channel.sendToQueue(queueName, Buffer.from(JSON.stringify(message)));
            console.log("Gonderilen Mesaj:", i.id);
        })
        //================INTERVAL===================================================================
        // setInterval(()=> {
        //     message.description = new Date().getTime();
        //     channel.sendToQueue(queueName, Buffer.from(JSON.stringify(message)));
        //     console.log('Gonderilen mesaj:',message);
        // }, 500);
        //========================INTERVAL===========================================================
    } catch (error) {
        console.log(error);        
    }
}