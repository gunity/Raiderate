const dateFormatter = new Intl.DateTimeFormat("en-GB", {
  timeZone: "UTC",
  day: "2-digit",
  month: "short",
  year: "2-digit",
  hour: "2-digit",
  minute: "2-digit",
  hour12: false,
});

export default function getLocalDateTime(value: string) {
  return dateFormatter.format(new Date(value)).replace(",", "");
}
