/* eslint-disable react/prop-types */
export default function StudentsTable(props) {

    return (
        <div className="table-section">
            <h3>Students List: {props.students.length} Students</h3>
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
                    {props.students.map((student) => (
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