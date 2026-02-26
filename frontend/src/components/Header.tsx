"use client";

import Link from "next/link";
import {usePathname} from "next/navigation";

export default function Header() {

    const path = usePathname();

    return (
        <div
            className="border-b h-14 flex items-center justify-between px-2"
        >
            <Link href="/">
                Raiderate
            </Link>
            <div
                className="flex items-center gap-2"
            >
                <Link
                    href="/login"
                    hidden={path === "/login"}
                    className="border rounded p-2 cursor-pointer"
                >
                    Login
                </Link>
                <Link
                    href="/register"
                    hidden={path === "/register"}
                    className="border rounded p-2 cursor-pointer"
                >
                    Register
                </Link>
            </div>
        </div>
    );
}