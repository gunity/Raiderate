import "server-only";

import isAdmin from "@/shared/common/api";
import {notFound} from "next/navigation";

export default async function Page() {
    const adminPrivileges = await isAdmin();

    if (!adminPrivileges) {
        notFound();
    }

    const Page = (await import("@/pages/AdminRatingReasons")).default;
    return <Page/>;
}