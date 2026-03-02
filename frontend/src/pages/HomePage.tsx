import Link from "next/link";

export default function HomePage() {

    return (
        <div className="m-5">
            <Link
                href="/rate"
                className="border rounded p-2"
            >
                Rate player
            </Link>
        </div>
    );
}

