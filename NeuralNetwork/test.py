from NeuralNetwork import * 


nn = NeuralNetwork()

error = lambda y,t: (t-y)**2
error_derivative = lambda y,t: -2*(t-y) # derivative with respect to y
alpha = 0.1
n_iter = 10
x = [[0,0], [0,1], [1,0], [1,1]]
y = [[1],[0],[0],[1]]

# select always next sample from the training 
i = -1 
def batch_selection(x, y):
    global i
    if i > 3:
        i=-1
    i += 1
    return x[i:i+1], y[i:i+1]

nn.add_input_layer(2, 5, lambda x: 1 / (1 + np.exp(-x)), lambda x: np.exp(-x) / ((1 + np.exp(-x) ** 2)))
nn.add_layer(1, lambda x: 1 / (1 + np.exp(-x)), lambda x: np.exp(-x) / ((1 + np.exp(-x) ** 2)))
nn.train(x, y, error_derivative, alpha, n_iter, batch_selection)


print("Training finished ______________________")
print(nn.forward([0,0]))
print(nn.forward([0,1]))
print(nn.forward([1,0]))
print(nn.forward([1,1]))

print("Linear regression ________________________")
nn = NeuralNetwork()
nn.add_input_layer(1, 5, lambda x: x, lambda x: 1)
nn.add_layer(1, lambda x: x, lambda x: 1)

x = [[0], [1], [5], [10]]
y = [[0], [1], [5], [10]]
nn.train(x, y, error_derivative, alpha, 4, batch_selection)
print("Training finished ______________________")
print(nn.forward([0]))
print(nn.forward([1]))
print(nn.forward([4]))
print(nn.forward([7]))
