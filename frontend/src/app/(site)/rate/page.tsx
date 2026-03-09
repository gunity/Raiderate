import RatePage from "@/pages/RatePage";
import { getAllActiveRatingReasonsServer } from "@/shared/ratings/api.server";

export default async function Page() {
  const ratingReasons = await getAllActiveRatingReasonsServer();

  return <RatePage reasons={ratingReasons} />;
}
