/* eslint-disable no-unused-vars */
import { useEffect, useState } from 'react';
import StudentsTable from './Components/StudentsTable';
import FormData from './Components/FormData';
import Header from './Components/Header';
import Swal from 'sweetalert2';

const App = () => {
    const API_URL = "https://localhost:7145/api/StudentsAPI";
    const headers = { "Content-Type": "application/json" };

    const [students, setStudents] = useState([]);
    const [newStudent, setNewStudent] = useState({ name: '', age: '', grade: '', gender: '', nationality: '' });
    const [editStudent, setEditStudent] = useState(null);

    const [average, setAverage] = useState(null);

    const [loading, setLoading] = useState(false);

    
      useEffect(() => {
        const fetchData = async () => {
            setLoading(true); // Start loading
            try {
                const studentsResponse = await fetch(`${API_URL}/AllStudents`);
                const studentsData = await studentsResponse.json();
                setStudents(studentsData);

                const averageResponse = await fetch(`${API_URL}/Average`);
                const averageData = await averageResponse.json();
                setAverage(averageData); // Add a state for the average
            } catch (error) {
                alert("Error fetching data: ", error);
            } finally {
                setLoading(false); // end loading
            }
        };
        fetchData();
    }, []);

    const handleAddStudent = (newStudent) => {
        fetch(API_URL, {
            method: "POST",
            headers,
            body: JSON.stringify(newStudent)
        }).then(response => response.json())
            .then(data => {
                setStudents([...students, data]); // Add to the current students list
                setNewStudent({ name: '', age: '', grade: '', gender: '', nationality: '' }); // Reset input fields
            })
            .catch(error => console.error("Error adding student:", error));
    };

    // Update Student
    const handleUpdateStudent = (newStudent) => {
        if (!editStudent) return;
        fetch(`${API_URL}/${editStudent.id}`, {
            method: "PUT",
            headers,
            body: JSON.stringify(newStudent)
        }).then(response => response.json())
            .then((data) => {
                const updatedStudents = students.map(student => { return (student.id === editStudent.id ? data : student) });
                setStudents(updatedStudents);
                setEditStudent(null); // Reset edit state
            }).catch(error => console.error("Error updating student:", error));
    };   

    function handleDelete(id) {
        Swal.fire({
            title: 'Confirm Deletion',
            text: "You won't be able to undo this!, Are you sure to delete it?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
        }).then((result) => {
            if (result.isConfirmed) {
                handleDeleteStudent(id);
                Swal.fire('Deleted!', 'The student has been deleted.', 'success');
            }
        });
    }


    // Delete Student
    const handleDeleteStudent = (id) => {
        fetch(`${API_URL}/${id}`,
            { method: 'DELETE' })
            .then(() => {
                setStudents(students.filter(
                    student => student.id !== id));
            })
            .catch(error => console.error("Error deleting student:", error));
    };

    function addOrUpdateStudent(formData) {

        if (editStudent) {
            handleUpdateStudent(formData);
        } else {
            handleAddStudent(formData);
        }

    }

    return (
        <>
            <Header />
            <div className="container">
                {loading && <div className="spinner"></div>}
                {!loading && <div className="main">
                        <FormData editStudent={editStudent} addOrUpdateStudent={addOrUpdateStudent} />
                        <StudentsTable students={students} average={average} setEditStudent={setEditStudent}
                            handleDeleteStudent={handleDelete} />

                    </div>
                }
            </div>

        </>
        

    );
};

export default App;

