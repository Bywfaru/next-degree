export type ActionResponse<T> = {
  data: T;
  success: true;
} | {
  error: string;
  success: false;
}