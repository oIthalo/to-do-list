﻿using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebAPI.Test;

public class ToDoListClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public ToDoListClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    protected async Task<HttpResponseMessage> DoPost(string method, object request, string token = "")
    {
        AuthorizeRequest(token);

        return await _httpClient.PostAsJsonAsync(method, request);
    }

    protected async Task<HttpResponseMessage> DoPut(string method, object request, string token = "", string id = "")
    {
        AuthorizeRequest(token);

        return await _httpClient.PutAsJsonAsync($"{method}/{id}", request);
    }

    protected async Task<HttpResponseMessage> DoGet(string method, string token = "", string id = "")    
    {
        AuthorizeRequest(token);

        return await _httpClient.GetAsync($"{method}/{id}");
    }

    protected async Task<HttpResponseMessage> DoDelete(string method, string id = "", string token = "")
    {
        AuthorizeRequest(token);

        return await _httpClient.DeleteAsync($"{method}/{id}");
    }

    private void AuthorizeRequest(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}