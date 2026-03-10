"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import React, { useEffect } from "react";
import { identityLogout } from "@/shared/identity/api";
import { useAuthStore } from "@/shared/auth/store";
import { LogIn, LogOut, UserPlus } from "lucide-react";

export default function Header() {
  const path = usePathname();

  const self = useAuthStore((s) => s.self);
  const loading = useAuthStore((s) => s.loading);
  const loadSelf = useAuthStore((s) => s.loadSelf);
  const logoutLocal = useAuthStore((s) => s.logout);

  useEffect(() => {
    void loadSelf();
  }, [loadSelf]);

  const logout = async () => {
    await identityLogout();
    logoutLocal();
  };

  const headerActions = () => {
    if (loading) {
      return <></>;
    }

    if (!self) {
      return (
        <>
          <Link
            href="/login"
            hidden={path === "/login"}
            className="inline-flex cursor-pointer items-center gap-2 p-2 hover:text-[#ffbe00]"
          >
            <LogIn className="h-5 w-5" />
            Login
          </Link>
          <Link
            href="/register"
            hidden={path === "/register"}
            className="inline-flex cursor-pointer items-center gap-2 p-2 hover:text-[#ffbe00]"
          >
            <UserPlus className="h-5 w-5" />
            Register
          </Link>
        </>
      );
    }

    return (
      <button
        className="inline-flex cursor-pointer items-center gap-2 p-2 hover:text-[#ffbe00]"
        onClick={logout}
      >
        <LogOut className="h-5 w-5" />
        Logout
      </button>
    );
  };

  return (
    <div className="flex h-14 items-center justify-between border-b border-[#1d2226] bg-black px-6">
      <Link href="/" className="font-bold text-[#eee4d0]">
        Raiderate
      </Link>
      <div className="flex items-center gap-2">{headerActions()}</div>
    </div>
  );
}
