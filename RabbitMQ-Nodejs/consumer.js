import amqp from "amqplib";
import data from "./data.json" assert {type:"json"};

const queueName = process.argv[2] || 'jobsQueue';

connect_rabbitmq();//hoisting.

//TIP: 1.First we should connect to AMQP for RabbitMQ uses AMQP.
async function connect_rabbitmq(){

    try {
        const connection = await amqp.connect("amqp://localhost:5672");//its default port.
        const channel = await connection.createChannel(); //create the channel
        const assertion = await channel.assertQueue("jobsQueue");//assert the queue,
    
        //consume the message
        console.log('mesaj bekleniyor...');
        channel.consume(queueName, (message)=> {
            const messageInfo = JSON.parse(message.content.toString());
            const userInfo = data.find(user => user.id == messageInfo.description);
            if(userInfo){
                console.log("Islenen Kayit:", userInfo);
                channel.ack(message);
            }
        })

        //===============================INTERVAL====================================
        // channel.consume(queueName, (message)=> {
        //     console.log('Messsage:', message.content.toString());
        //     channel.ack(message);//INFO: telling rabbit mq that we've resolved this message.
        // });
    } catch (error) {
        console.log(error);        
    }
}