using System;
using System.Collections.Generic;

namespace Tweet_Counts_Per_Frequency
{
  class Program
  {
    static void Main(string[] args)
    {
      TweetCounts tweetCounts = new TweetCounts();
      tweetCounts.recordTweet("tweet3", 0);
      tweetCounts.recordTweet("tweet3", 60);
      tweetCounts.recordTweet("tweet3", 10);
      var answer = tweetCounts.getTweetCountsPerFrequency("minute", "tweet3", 0, 59);
      Console.WriteLine(string.Join(",", answer));
      answer = tweetCounts.getTweetCountsPerFrequency("minute", "tweet3", 0, 60);
      Console.WriteLine(string.Join(",", answer));
      tweetCounts.recordTweet("tweet3", 120);
      answer = tweetCounts.getTweetCountsPerFrequency("hour", "tweet3", 0, 210);
      Console.WriteLine(string.Join(",", answer));
    }
  }

  class TweetCounts
  {
    Dictionary<string, List<int>> map;
    public TweetCounts()
    {
      map = new Dictionary<string, List<int>>();
    }

    public void recordTweet(string tweetName, int time)
    {
      if (!map.ContainsKey(tweetName))
      {
        List<int> temp = new List<int>();
        temp.Add(time);
        map.Add(tweetName, temp);
      }
      else
      {
        map[tweetName].Add(time);
      }

    }

    public List<int> getTweetCountsPerFrequency(string freq, string tweetName, int startTime, int endTime)
    {

      int interval = 60;
      if (freq.Equals("hour"))
        interval = interval * 60;
      if (freq.Equals("day"))
        interval = interval * 60 * 24;
      List<int> res = new List<int>();

      // get the number of possible intervals, 
      // if startTime = 30 and endTime = 150 with minute as freq
      // (150 - 30) / 60 = 2, this means there will be 3 intervals
      // [30, 90); [90, 150); [150, 150)
      for (int i = 0; i <= (endTime - startTime) / interval; i++)
        res.Add(0);

      List<int> times = map[tweetName];
      foreach (int time in times)
      {
        if (startTime <= time && time <= endTime)
        {
          // get the index of which interval at current time
          int idx = (time - startTime) / interval;
          res[idx] = res[idx] + 1;
        }
      }
      return res;
    }
  }

  /**
   * Your TweetCounts object will be instantiated and called as such:
   * TweetCounts obj = new TweetCounts();
   * obj.recordTweet(tweetName,time);
   * List<Integer> param_2 = obj.getTweetCountsPerFrequency(freq,tweetName,startTime,endTime);
   */
}
