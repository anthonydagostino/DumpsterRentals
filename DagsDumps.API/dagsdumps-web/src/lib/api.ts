export const apiBase = (import.meta.env.VITE_API_BASE_URL as string).replace(/\/$/, "");

async function handle<T>(res: Response): Promise<T> {
  if (!res.ok) throw new Error(`${res.status} ${res.statusText}`);
  return res.json() as Promise<T>;
}

export async function get<T>(path: string): Promise<T> {
  const res = await fetch(`${apiBase}/${path.replace(/^\//, "")}`);
  return handle<T>(res);
}

export async function post<TBody, TResp>(path: string, body: TBody): Promise<TResp> {
  const res = await fetch(`${apiBase}/${path.replace(/^\//, "")}`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  });
  return handle<TResp>(res);
}
