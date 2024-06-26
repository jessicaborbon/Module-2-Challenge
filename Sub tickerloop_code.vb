Sub tickerloop()

'Loop through all the sheets.
    For Each ws In Worksheets

    'Setting up the stage

        'Set a variable for holding the ticker name, the column of interest
         'Set a varable for holding a total count on the total volume of trade
         'Keep track of the location for each ticker name in the summary table
        Dim tickername As String
    

        Dim tickervolume As Double
        tickervolume = 0

        Dim summary_ticker_row As Integer
        summary_ticker_row = 2
        
        'Note: Yearly Change is simply the difference: (Close Price at the end of a trading year - Open Price at the beginning of the trading year)
        'Percent change is a simple percent change -->((Close - Open)/Open)*100
        'Set initial open_price. Other opening prices will be determined in the conditional loop.
        Dim open_price As Double

        open_price = ws.Cells(2, 3).Value
        
        Dim close_price As Double
        Dim yearly_change As Double
        Dim percent_change As Double

        'Label the Summary Table headers
        ws.Cells(1, 9).Value = "Ticker"
        ws.Cells(1, 10).Value = "Yearly Change"
        ws.Cells(1, 11).Value = "Percent Change"
        ws.Cells(1, 12).Value = "Total Stock Volume"

        'Count the number of rows in the first column.
        lastrow = ws.Cells(Rows.Count, 1).End(xlUp).Row

        'Loop rows by the ticker names
        ' ticker names are sorted and are string variables.


        For i = 2 To lastrow

            'Searches for when the value of the next cell is different than that of the current cell
            If ws.Cells(i + 1, 1).Value <> ws.Cells(i, 1).Value Then
        
              'Set the ticker name
              tickername = ws.Cells(i, 1).Value

              'Add the volume of trade
              tickervolume = tickervolume + ws.Cells(i, 7).Value

              'Print the ticker name in the summary table
              ws.Range("I" & summary_ticker_row).Value = tickername

              'Print the trade volume for each ticker in the summary table
              ws.Range("L" & summary_ticker_row).Value = tickervolume

              'Now collect information about closing price
              close_price = ws.Cells(i, 6).Value

              'Calculate yearly change
               yearly_change = (close_price - open_price)
              
              'Print the yearly change for each ticker in the summary table
              ws.Range("J" & summary_ticker_row).Value = yearly_change

              'Check for the non-divisibilty condition when calculating the percent change
                If open_price = 0 Then
                    percent_change = 0
                
                Else
                    percent_change = yearly_change / open_price
                
                End If

              'Print the yearly change for each ticker in the summary table
              ws.Range("K" & summary_ticker_row).Value = percent_change
              ws.Range("K" & summary_ticker_row).NumberFormat = "0.00%"
   
              'Reset the row counter. Add one to the summary_ticker_row
              summary_ticker_row = summary_ticker_row + 1

              'Reset volume of trade to zero
              tickervolume = 0

              'Reset the opening price
              open_price = ws.Cells(i + 1, 3)
            
            Else
              
               'Add the volume of trade
              tickervolume = tickervolume + ws.Cells(i, 7).Value

            
            End If
        
        Next i

    'Conditional formatting that will highlight positive change in green and negative change in red
    'First find the last row of the summary table

    lastrow_summary_table = ws.Cells(Rows.Count, 9).End(xlUp).Row
    
    'Color code yearly change
        For i = 2 To lastrow_summary_table
            
            If ws.Cells(i, 10).Value > 0 Then
                ws.Cells(i, 10).Interior.ColorIndex = 10
            
            Else
                ws.Cells(i, 10).Interior.ColorIndex = 3
            
            End If
        
        Next i

        
End Sub
