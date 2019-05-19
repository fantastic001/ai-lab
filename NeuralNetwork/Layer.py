
from Neuron import * 
import numpy as np 

class Layer:

    def __init__(self, n_inputs, n_outputs, f, df):
        self.neurons = list(Neuron(n_inputs, f, df) for i in range(n_outputs))
        self.n_inputs = n_inputs
        self.n_outputs = n_outputs
        self.f = f 
        self.df = df

    def forward(self, x):
        return np.array(list(n.forward(x) for n in self.neurons))

    def forward_no_activation(self, x):
        return np.array(list(n.forward_no_activatioon(x) for n in self.neurons))
    
    def backward(self, x):
        return np.array(list(n.df(n.forward_no_activation(x)) for n in self.neurons))
        
        
