import logging
import threading
import time


#INFO: 1. Thread, in this module, nicely encapsulates threads, providing a clean interface to work with them.

def thread_function(name):
    logging.info('Thread %s: starting', name)
    time.sleep(2)
    logging.info('Thread %s: finishing', name)


def thread_function2(name):
    logging.info('Thread %s: starting', name)
    time.sleep(5)
    logging.info('Thread %s: finishing', name)


'''
#INFO: Take __main__ function as a thread.
       x is another thead, by starting the thread x, you are creating the second workflow!
'''
if __name__ == "__main__":
    format = "%(asctime)s: %(message)s"
    logging.basicConfig(format = format, level= logging.INFO, datefmt="%H:%M:%S")#INFO: Non demonic!
    logging.info("Main    : before creating thread")
    y = threading.Thread(target=thread_function2, args=(2,)) #INFO: NON-DAEMONIC
    x = threading.Thread(target=thread_function, args=(1,), daemon=True) #INFO: DAEMONIC
    logging.info("Main    : before running thread")
    x.start()
    y.start()
    logging.info("Main    : wait for the thread to finish")
    x.join() #INFO: Helps us with main function to wait x threat to finish.
    y.join()
    logging.info("Main    : all done")