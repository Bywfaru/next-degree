export type ActionResponseSuccess<T> = {
  data: T;
  success: true;
}

export type ActionResponseError = {
  error: string;
  success: false;
}

export type ActionResponse<T> = ActionResponseSuccess<T> | ActionResponseError;