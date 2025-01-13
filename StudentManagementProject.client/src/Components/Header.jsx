import studentpic from "../assets/images/Student-Management-Project.jpg"

export default function Header() {
    return (
        <header>
            <img src={studentpic} alt="image-project" />
            <h1>Student Management project</h1>
         </header>
    )
}