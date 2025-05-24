const express = require('express');
const bodyParser = require('body-parser');
const { v4: uuidv4 } = require('uuid');

const app = express();
app.use(bodyParser.json());

// Clase para el nodo de la lista enlazada
class ListNode {
    constructor(value) {
        this.id = uuidv4();
        this.value = value;
        this.next = null;
    }
}

// Clase para la lista enlazada
class LinkedList {
    constructor() {
        this.head = null;
        this.size = 0;
    }

    // Agregar un nuevo nodo al final de la lista
    add(value) {
        const newNode = new ListNode(value);
        
        if (!this.head) {
            this.head = newNode;
        } else {
            let current = this.head;
            while (current.next) {
                current = current.next;
            }
            current.next = newNode;
        }
        
        this.size++;
        return newNode.id;
    }

    // Eliminar un nodo por su ID
    remove(id) {
        if (!this.head) return false;

        // Caso especial: eliminar el head
        if (this.head.id === id) {
            this.head = this.head.next;
            this.size--;
            return true;
        }

        let current = this.head;
        while (current.next) {
            if (current.next.id === id) {
                current.next = current.next.next;
                this.size--;
                return true;
            }
            current = current.next;
        }

        return false;
    }

    // Obtener todos los nodos como array
    getAll() {
        const nodes = [];
        let current = this.head;
        
        while (current) {
            nodes.push({
                id: current.id,
                value: current.value
            });
            current = current.next;
        }
        
        return nodes;
    }
}

// Instancia global de la lista enlazada
const linkedList = new LinkedList();

// Endpoints
app.get('/linked-list/', (req, res) => {
    res.json(linkedList.getAll());
});

app.post('/linked-list/', (req, res) => {
    const { value } = req.body;
    if (!value) {
        return res.status(400).json({ error: 'Se requiere el campo "value" en el cuerpo de la solicitud' });
    }
    
    const id = linkedList.add(value);
    res.json({ id });
});

app.delete('/linked-list/:id', (req, res) => {
    const { id } = req.params;
    const success = linkedList.remove(id);
    
    if (success) {
        res.json({ message: 'Nodo eliminado correctamente' });
    } else {
        res.status(404).json({ error: 'Nodo no encontrado' });
    }
});

// Iniciar el servidor
const PORT = 3000;
app.listen(PORT, () => {
    console.log(`Servidor NodeJS corriendo en http://localhost:${PORT}`);
});