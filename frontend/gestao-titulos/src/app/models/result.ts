export interface Result<T> {
  isSuccess: boolean;
  messages: string[];
  data: T;
}
