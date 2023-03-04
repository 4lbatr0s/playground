import { createClient } from 'redis';

const client = createClient();
const listenerClient = createClient();
client.on('error', (err) => console.log('Redis Client Error', err));

(async () => {
    try {
        await client.connect();

        //set
        const return1 = await client.set('user_name', '4lbartr0s');
        console.log(return1);

        //get
        const value = await client.get('user_name');
        console.log(value);

        //del, returns 1 if successful, 0 otherwise.
        const deletedValue = await client.del('user_name');
        console.log('Delete:', deletedValue);

        //exists returns 1 if successful, 0 otherwise.
        const return2 = await client.exists('user_name');
        console.log('Exists:', return2);

        //append, returns lenght of append operation.
        const lastName = await client.append('last_name', 'oner');
        console.log('Last Name:', lastName);
        const lastName2 = await client.append('last_name', 'oner oner oner');
        console.log('Last Name:', lastName2);

        const lastNameValue = await client.get('last_name');
        console.log('Last Name:', lastNameValue);

        //keys *
        const allKeys = await client.keys('*');
        console.log('All:', allKeys);

        //sub-pub
        const listener = (channel, message) => console.log(channel, message); //event.on
        const subscriber = listenerClient.duplicate();
        await subscriber.connect();
        await subscriber.subscribe('user_tv', listener)

        await client.publish('user_tv', 'He is not the messiah, he is a very naughty boy!'); //event.emitter() 
        await client.publish('user_tv', 'There you go redis cowboy!'); //event.emitter() 

    } catch (error) {
        console.log('Error:', error);
    }
    await client.disconnect();
})();
