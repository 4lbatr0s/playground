import logging
import threading
import time
import concurrent.futures



class FakeDatabase:
    def __init__(self) -> None:
        self.value = 0
        
    def update(self,name):
        logging.info("Thread %s: starting update", name)
        local_copy = self.value
        local_copy +=1
        time.sleep(0.1)
        self.value = local_copy
        logging.info("Thread %s: starting update", name)
        

if __name__ == "__main__":
    format = "%(asctime)s: %(message)s"
    logging.basicConfig(format=format, level=logging.INFO,
                        datefmt="%H:%M:%S")
    database = FakeDatabase()
    logging.info("Testing update. Starting value is %d.", database.value)
    with concurrent.futures.ThreadPoolExecutor(max_workers=2) as executor:
        for index in range(2):
            executor.submit(database.update, index) #INFO: create two threads and have them to execute the database.update() function
    logging.info("Testing update. Ending value is %d.", database.value)
