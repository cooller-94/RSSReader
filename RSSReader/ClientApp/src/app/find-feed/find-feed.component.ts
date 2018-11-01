import { Component, OnInit } from '@angular/core';
import { FeedService } from '../shared/services/feed.service';
import { SearchFeedResultModel } from '../shared/models/search-feed-result.model';

@Component({
  selector: 'app-find-feed',
  templateUrl: './find-feed.component.html',
  styleUrls: ['./find-feed.component.css']
})
export class FindFeedComponent implements OnInit {

  public searchResult: SearchFeedResultModel[];
  public message: string;

  constructor(private feedService: FeedService) { }

  public ngOnInit() {
  }

  public find(term: string): void {
    if (!term || term.trim().length < 3) {
      return;
    }

    this.feedService.findFeeds(term).subscribe(feeds => {
      if (feeds) {
        this.message = null;
        this.searchResult = feeds;
      } else {
        this.searchResult = null;
        this.message = `There are no feeds for <strong>${term}</strong>`;
      }
    });
  }
}
