import { Feed } from "./feed.model";

export interface Post {
  title: string;
  description: string;
  link: string;
  isRead: boolean;
  publishDate: Date;
  author: string;
  commentsUrl: string;
  feed: Feed;
  publishAgo: string;
}
