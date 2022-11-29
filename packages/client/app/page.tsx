import styles from './page.module.css'
import Link from "next/link";

export default function Home() {

  const today = new Date();

  return (
    <div className={styles.container}>
      <h1>Hello, Next.js</h1>
      <ul>
        <li><Link href={`/calendar/${today.getFullYear()}`}>This Year</Link></li>
        <li><Link href={`/calendar/${today.getFullYear()}/${today.getMonth() + 1}`}>This Month</Link></li>
      </ul>
    </div>
  )
}
