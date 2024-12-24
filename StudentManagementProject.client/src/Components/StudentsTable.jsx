/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable react/prop-types */

import { useState, useEffect } from "react"
export default function StudentsTable(props) {

    const [filterStudents, setFilterStudents] = useState(null);
    const [inputValue, setInputValue] = useState(""); // Stores the input value
    const [searchBy, setSearchBy] = useState(""); // Stores the selected search field   
    
    const averageFilterTable = filterStudents ? Math.round(filterStudents.reduce((sum, num) => sum + num.grade, 0) / filterStudents.length,2) : 0;
    
    useEffect(() => {
        setFilterStudents(props.students)
    }, [props.students, searchBy])


    const handleChange = (e) => {
        if (e.target.value === "passedStudent") {
            setSearchBy(e.target.value);
            setInputValue(1); // any value 
        } else {
            setSearchBy(e.target.value);
        }
         
    }
    useEffect(() => {

        const filter = props.students.filter((student) => {
            // Filter based on the selected field
            if (searchBy === "id") {
                return student.id === parseInt(inputValue, 10);
            } else if (searchBy === "gender") {
                return student.gender === (inputValue.toLowerCase() === "female" ? 0 : 1);
            } else if (searchBy === "name") {
                return student.name.toLowerCase().startsWith(inputValue.toLowerCase());
            } else if (searchBy === "nationality") {
                return student.nationality.toLowerCase().startsWith(inputValue.toLowerCase());
            } else if (searchBy === "passedStudent") {
                return student.grade >= 50;
            }
            return student;
        });

        setFilterStudents(filter);

    }, [inputValue])

   

    return (
        <div className="table-section">
            <div className="info-table">
                
                    <span className="info-table-span">Nbr of Students: {filterStudents?.length ||props.students.length} </span>
                    <span className="info-table-span">Average Grade: {averageFilterTable || (props.average !== null ? props.average : 'Calculating...')}</span>         
               
                    <fieldset>
                        <legend>Search by:</legend>
                        <select
                            id="search-by-select"
                            name="search-input"
                            value={searchBy}
                            onChange={handleChange}
                            required
                        >
                            <option value="">--</option>                            
                            <option key="id" value="id">Id</option>
                            <option key="name" value="name">Name</option>
                            <option key="name" value="gender">Gender</option>
                            <option key="nationality" value="nationality">Nationality</option>
                            <option key="passedStudent" value="passedStudent">PassedStudent</option>
                        </select>
                    {searchBy && searchBy !=="passedStudent" &&
                        <input
                        id="search-by-input"
                        type={searchBy === "id" ? "number" : "text"}
                        placeholder="Enter your data.."
                        onInput={e => setInputValue(e.target.value)}                        
                        required

                            /> }
                </fieldset>

            </div>
             <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Age</th>
                        <th>Grade</th>
                        <th>Gender</th>
                        <th>Nationality</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    {filterStudents?.map((student) => (
                        <tr key={student.id}>
                            <td>{student.id}</td>
                            <td>{student.name}</td>
                            <td>{student.age}</td>
                            <td>{student.grade}</td>
                            <td>{student.gender ? "Male" : "Female"}</td>
                            <td>{student.nationality}</td>
                            <td>
                                <button
                                    className="btn btn-primary btn-sm"
                                    onClick={() => props.setEditStudent(student)}
                                >
                                    Edit
                                </button>
                                <button
                                    className="btn btn-danger btn-sm"
                                    onClick={() => props.handleDeleteStudent(student.id)}
                                >
                                    Delete
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}