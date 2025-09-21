import axios from 'axios';

const API_URL = 'http://localhost:5119/api';

export const getAuctions = async () => {
  try {
    const response = await axios.get(`${API_URL}/auctions`);
    return response.data;
  } catch (error) {
    console.error('Error fetching auctions:', error);
    throw error;
  }
};

export const getAuctionById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}/auctions/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching auction ${id}:`, error);
    throw error;
  }
};

export const createAuction = async (auctionData) => {
  try {
    const response = await axios.post(`${API_URL}/auctions`, auctionData);
    return response.data;
  } catch (error) {
    console.error('Error creating auction:', error);
    throw error;
  }
};

export const getUserAuctions = async () => {
  try {
    const response = await axios.get(`${API_URL}/auctions/user`);
    return response.data;
  } catch (error) {
    console.error('Error fetching user auctions:', error);
    throw error;
  }
};

export const getBidsByAuctionId = async (auctionId) => {
  try {
    const response = await axios.get(`${API_URL}/bids?auctionId=${auctionId}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching bids for auction ${auctionId}:`, error);
    throw error;
  }
};

export const createBid = async (bidData) => {
  try {
    const response = await axios.post(`${API_URL}/bids`, bidData);
    return response.data;
  } catch (error) {
    console.error('Error creating bid:', error);
    throw error;
  }
};

export const getUserBids = async () => {
  try {
    const response = await axios.get(`${API_URL}/bids/user`);
    return response.data;
  } catch (error) {
    console.error('Error fetching user bids:', error);
    throw error;
  }
};