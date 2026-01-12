const API_URL = 'http://localhost:5279/api/students';

// Load students when page loads
document.addEventListener('DOMContentLoaded', () => {
    loadStudents();
    
    document.getElementById('studentForm').addEventListener('submit', (e) => {
        e.preventDefault();
        const id = document.getElementById('studentId').value;
        
        if (id) {
            updateStudent(id);
        } else {
            createStudent();
        }
    });
});

// CREATE - Add new student
async function createStudent() {
    const student = {
        name: document.getElementById('name').value,
        email: document.getElementById('email').value,
        course: document.getElementById('course').value,
        age: parseInt(document.getElementById('age').value)
    };

    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(student)
        });

        if (response.ok) {
            alert('✓ Student added successfully!');
            clearForm();
            loadStudents();
        } else {
            alert('✗ Failed to add student');
        }
    } catch (error) {
        alert('✗ Error: ' + error.message);
    }
}

// READ - Load all students
async function loadStudents() {
    try {
        const response = await fetch(API_URL);
        const students = await response.json();
        
        const tbody = document.getElementById('studentTableBody');
        tbody.innerHTML = '';

        if (students.length === 0) {
            tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">No students found</td></tr>';
            return;
        }

        students.forEach(student => {
            const row = `
                <tr>
                    <td>${student.id}</td>
                    <td>${student.name}</td>
                    <td>${student.email}</td>
                    <td>${student.course}</td>
                    <td>${student.age}</td>
                    <td>
                        <button class="btn btn-edit" onclick="editStudent(${student.id})">Edit</button>
                        <button class="btn btn-delete" onclick="deleteStudent(${student.id})">Delete</button>
                    </td>
                </tr>
            `;
            tbody.innerHTML += row;
        });
    } catch (error) {
        alert('✗ Error loading students: ' + error.message);
    }
}

// UPDATE - Edit student
async function editStudent(id) {
    try {
        const response = await fetch(`${API_URL}/${id}`);
        const student = await response.json();

        document.getElementById('studentId').value = student.id;
        document.getElementById('name').value = student.name;
        document.getElementById('email').value = student.email;
        document.getElementById('course').value = student.course;
        document.getElementById('age').value = student.age;

        // Show update button, hide create button
        document.querySelector('.btn-create').style.display = 'none';
        document.getElementById('updateBtn').style.display = 'block';
    } catch (error) {
        alert('✗ Error: ' + error.message);
    }
}

async function updateStudent(id) {
    const student = {
        id: parseInt(id),
        name: document.getElementById('name').value,
        email: document.getElementById('email').value,
        course: document.getElementById('course').value,
        age: parseInt(document.getElementById('age').value)
    };

    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(student)
        });

        if (response.ok) {
            alert('✓ Student updated successfully!');
            clearForm();
            loadStudents();
        } else {
            alert('✗ Failed to update student');
        }
    } catch (error) {
        alert('✗ Error: ' + error.message);
    }
}

// DELETE - Remove student
async function deleteStudent(id) {
    if (!confirm('Are you sure you want to delete this student?')) {
        return;
    }

    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            alert('✓ Student deleted successfully!');
            loadStudents();
        } else {
            alert('✗ Failed to delete student');
        }
    } catch (error) {
        alert('✗ Error: ' + error.message);
    }
}

// Clear form
function clearForm() {
    document.getElementById('studentId').value = '';
    document.getElementById('studentForm').reset();
    document.querySelector('.btn-create').style.display = 'block';
    document.getElementById('updateBtn').style.display = 'none';
}