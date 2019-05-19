
import random
import numpy as np
class Neuron:
    
    def __init__(self, n_inputs, f, df):
        self.n_inputs = n_inputs 
        self.f = f 
        self.df = df 
        self.w = np.array(list([1000 * random.random() for x in range(n_inputs+1)]))

    def forward_no_activation(self, x):
        x = np.concatenate((np.array([1]), np.array(x)))
        return self.w.dot(x)

    def forward(self, x):
        return self.f(self.forward_no_activation(x))

