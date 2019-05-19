
from Layer import *

class NeuralNetwork:

    def __init__(self):
        self.layers = []

    def add_input_layer(self, n_inputs, n_outputs, f, df):
        self.layers = [Layer(n_inputs, n_outputs, f, df)]
    
    def add_layer(self, n_outputs, f, df):
        self.layers.append(Layer(self.layers[-1].n_outputs, n_outputs, f, df))

    def forward(self, x):
        for l in self.layers:
            x = l.forward(x)
        return x

    def get_errors(self, x, actual_y, error_derivative):
        y = x
        a = [y]
        for l in self.layers:
            a.append(l.forward(y))
            y = a[-1]
        errors = []
        errors.append(error_derivative(a[-1], actual_y * self.layers[-1].backward(a[-2])))
        for l, current_layer, xx in zip(reversed(self.layers[1:]), reversed(self.layers[:-1]), reversed(a[:-2])):
            print("Calculating errors for hidden layer %d" % (len(self.layers) - len(errors)))
            W = list(n.w for n in l.neurons)
            W = np.array(W)
            errors.append(W.T.dot(np.array(errors[-1])) * np.concatenate([np.array([1]), current_layer.backward(xx)]))
            print(errors[-1])
        return (a, list(reversed(errors)))

    def train_step(self, x,y, error_derivative, alpha):
        a, errors = self.get_errors(x, y, error_derivative)
        for li, l in enumerate(self.layers):
            for ni, n in enumerate(l.neurons):
                n.w -= alpha * errors[li][ni] * np.concatenate([[1], a[li]])

    def batch(self, data, y, error_derivative, alpha):
        for d, t in zip(data, y):
            self.train_step(d,t, error_derivative, alpha)

    def train(self, data, y, error_derivative, alpha, n_iter, batch_selection):
        for t in range(n_iter):
            x, yy = batch_selection(data, y)
            for xx, t in zip(x, yy):
                print("TRaining for %s -> %s" % (str(xx), str(t)))
                self.batch([xx], [t], error_derivative, alpha)
