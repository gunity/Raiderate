import "server-only";

import isAdmin from "@/shared/common/api";
import { notFound } from "next/navigation";
import { getAllRatingReasonsServer } from "@/shared/ratings/api.server";

export default async function Page() {
  const adminPrivileges = await isAdmin();

  if (!adminPrivileges) {
    notFound();
  }

  const reasons = await getAllRatingReasonsServer();

  const Page = (await import("@/pages/AdminRatingReasons")).default;
  return <Page reasons={reasons} />;
}
