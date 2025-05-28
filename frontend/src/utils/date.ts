export function parseDate(dateString: string): Date {
  const fixedDateString = dateString.replace(/\.(\d{3})\d+/, ".$1");
  return new Date(fixedDateString);
}
