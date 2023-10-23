import amqp from "amqplib";

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
        const assertion = await channel.assertQueue("jobsQueue");//assert the queue,
        setInterval(()=> {
            message.description = new Date().getTime();
            channel.sendToQueue(queueName, Buffer.from(JSON.stringify(message)));
            console.log('Gonderilen mesaj:',message);
        }, 500);
    } catch (error) {
        console.log(error);        
    }
}