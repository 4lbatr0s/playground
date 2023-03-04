import { createClient } from 'redis';

(async () => {
    const client = createClient();
    client.on('error', (err) => console.log('Redis Client Error', err));

    try {
        await client.connect();
        await client.publish('developer_tv', "app uzerinden gonderilen", (e,number)=> {
            console.log(`mesaj ${number} kisiye gonderildi`);
        });
    } catch (error) {
        console.log(error);
    }

    await client.disconnect();
})();
